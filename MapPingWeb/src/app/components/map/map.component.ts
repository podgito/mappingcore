import { Component, OnInit, AfterContentInit } from '@angular/core';
import * as d3 from 'd3v4';
import * as topojson from 'topojson';
import { WindowRefService } from '@services/window-ref.service';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Ping } from '@models/ping';
import { GeolocationService } from '@services/geolocation.service';
import { SignalrService } from '@services/signalr.service';
import { D3SvgService } from '@services/d3-svg.service';

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
  constructor(private windowRef: WindowRefService,
    private geoService: GeolocationService,
    private signalRService: SignalrService,
    private d3Svg: D3SvgService) { }


  ngOnInit() {
    this.signalRService.init();

    $this = this;

    this.signalRService.events.subscribe(event => this.addEvent(event));

    this.geoService.getLocation();
  }


  ngAfterContentInit() {
    //TODO D3
    this.counties = [];





    this.svg = this.d3Svg.createSvg('#map-container');


    // var filename = 'data/ireland.json';
    // var filename = 'data/gb_admin.json';
    var filename = 'data/gb_counties.json';
    d3.json(filename, function (error, c) {

      var window = $this.windowRef.nativeWindow;
      var containerHeight = window.innerHeight - 62;
      $this.projection = d3.geoMercator()
        .translate([window.innerWidth / 2, containerHeight / 2]);

      var topology = topojson.feature(c, c.objects.regions);

      var path = d3.geoPath()
        .projection($this.projection);

      var bounds = d3.geoBounds(topology),
                  center = d3.geoCentroid(topology);
              // Compute the angular distance between bound corners
      var distance = d3.geoDistance(bounds[0], bounds[1]),
          scale = containerHeight / distance / Math.sqrt(2);


        console.log(center);

      $this.projection.scale(scale).center(center);

      $this.svg.selectAll("path")
        .data(topojson.feature(c, c.objects.regions).features)
        .enter()
        .append("path")
        .attr("class", function (county) { // can add custom classes here
          //county.id is 'Antrim', 'Dublin' etc.
          //console.log(county);
          $this.counties.push(county);
          return "county"; // + county.id;
        })
        .attr("d", path);
    });

    this.svg.on("click", function () {
      var coords = d3.mouse(this);
      console.log(coords);
      var long = $this.projection(coords)[0];
      var lat = $this.projection(coords)[1];
      const p = new Ping(0, long, lat);
      console.log(p);
      $this.d3Svg.addAnimatedPoint($this.svg, coords[0], coords[1], 10); // a

      $this.signalRService.send(p).subscribe(_ => { });
    });

  }



  addEvent(event: Ping) {
    console.log('addEvent');
    console.log(event);
    var coords = [event.longitude, event.latitude];
    this.d3Svg.addAnimatedPoint(this.svg, this.projection(coords)[0], this.projection(coords)[1], event.value);
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
