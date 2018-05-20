import { Component, OnInit, AfterContentInit } from '@angular/core';
import * as d3 from 'd3v4';
import * as topojson from 'topojson';
import { WindowRefService } from '../../services/window-ref.service';
import { HubConnection, HubConnectionBuilder  } from '@aspnet/signalr';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit, AfterContentInit {

  hubUrl = 'http://localhost:60961/map';
  private hub: HubConnection;
  constructor(private windowRef: WindowRefService) { }

  ngOnInit() {
    // https://passos.com.au/signalr-with-net-core-2-1-and-angular/
    this.hub = new HubConnectionBuilder()
                  .withUrl(this.hubUrl)
                  .build();

    this.hub.on('event', (event) =>{
      console.log(event);
    });

    this.hub.start();
  }

  ngAfterContentInit() {
    //TODO D3
    var counties = [];
    var window = this.windowRef.nativeWindow;
    var containerHeight = window.innerHeight - 62;
    var projection = d3.geoMercator()
      .scale(5500 * containerHeight / 670)
      .center([-8, 53.45])
      .translate([window.innerWidth / 2, containerHeight / 2]);

    var svg = d3.select("#map-container").append("svg")
      .attr("width", '100%')
      .attr("height", containerHeight);

    var path = d3.geoPath()
      .projection(projection);


    d3.json("data/ireland.json", function (error, c) {

      // console.log(error);
      // console.log(c);
      // c.objects.counties.forEach(function (county) {
      //     counties.push(county.id);
      // }, this);

      svg.selectAll("path")
        .data(topojson.feature(c, c.objects.counties).features)
        .enter()
        .append("path")
        .attr("class", function (county) {
          //county.id is 'Antrim', 'Dublin' etc.
          console.log(county);
          counties.push(county);
          return "counties " + county.id;
        })
        .attr("d", path);


    });

  }

}
