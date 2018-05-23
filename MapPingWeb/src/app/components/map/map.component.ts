import { Component, OnInit, AfterContentInit } from '@angular/core';
import * as d3 from 'd3v4';
import * as topojson from 'topojson';
import { WindowRefService } from '../../services/window-ref.service';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

var $this;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit, AfterContentInit {

  hubUrl = 'http://localhost:60961/map';
  svg: any;
  projection: any;
  counties: any[];
  $this: MapComponent;
  private hub: HubConnection;
  constructor(private windowRef: WindowRefService) { }

  ngOnInit() {
    $this = this;
    // https://passos.com.au/signalr-with-net-core-2-1-and-angular/
    this.hub = new HubConnectionBuilder()
      .withUrl(this.hubUrl)
      .build();

    this.hub.on('event', (event) => {
      console.log(event);
    });

    // this.hub.start();
  }

  ngAfterContentInit() {
    //TODO D3
    this.counties = [];
    var window = this.windowRef.nativeWindow;
    var containerHeight = window.innerHeight - 62;
    this.projection = d3.geoMercator()
      .scale(5500 * containerHeight / 670)
      .center([-8, 53.45])
      .translate([window.innerWidth / 2, containerHeight / 2]);

    this.svg = d3.select("#map-container").append("svg")
      .attr("width", '100%')
      .attr("height", containerHeight);

    var path = d3.geoPath()
      .projection(this.projection);


    d3.json("data/ireland.json", function (error, c) {

      // console.log(error);
      // console.log(c);
      // c.objects.counties.forEach(function (county) {
      //     counties.push(county.id);
      // }, this);

      $this.svg.selectAll("path")
        .data(topojson.feature(c, c.objects.counties).features)
        .enter()
        .append("path")
        .attr("class", function (county) {
          //county.id is 'Antrim', 'Dublin' etc.
          console.log(county);
          $this.counties.push(county);
          return "county"; // + county.id;
        })
        .attr("d", path);
    });

    this.svg.on("click", function(){
      var coords = d3.mouse(this);
      $this.svg.append("circle")
        .attr("cx", coords[0])
        .attr("cy", coords[1])
        .attr("fill", "#900")
        .attr("stroke", "#999")
        .attr("opacity", 1)
        .attr("r", 0.1)
        .transition()
        .duration(1500)
        .attr("r", 20) //TODO make the ending size a function of the value
        .attr("opacity", 0)
        .on("end", function () {
          d3.select(this).remove()
        });
    });

  }

  addEvent(event) {
    var coords = [event.lon, event.lat];

    var county = this.getCounty(coords);

    console.log(county);

    $this.svg.append("circle")
      .attr("cx", this.projection(coords)[0])
      .attr("cy", this.projection(coords)[1])
      .attr("fill", "#900")
      .attr("stroke", "#999")
      .attr("opacity", 1)
      .attr("r", 0.1)
      .transition()
      .duration(1500)
      .attr("r", event.value) //TODO make the ending size a function of the value
      .attr("opacity", 0)
      .on("end", function () {
        d3.select(this).remove()
      });
  }

  //This is a backup, in case we don't get the county data.
  getCounty(coords) {
    var thisCounty;
    $this.counties.forEach(function (county) {
      if (d3.geoContains(county, coords)) {
        thisCounty = county;
      }
      return;
    }, this);
    return thisCounty;

  }

}
