import { Injectable } from '@angular/core';
import * as d3 from 'd3v4';
import * as topojson from 'topojson';

@Injectable({
  providedIn: 'root'
})
export class D3SvgService {

  constructor() { }

  createSvg(containerElement: string) {
    return d3.select("#map-container")
      .append("svg")
      .classed("svg-content-responsive", true)
      .attr("preserveAspectRatio", "xMinYMin meet")
      .attr("viewbox", "0 0 600 400");
  }

  private resizeSvg(){
    var width = parseInt(d3.select('#map-container').width);
    var height = parseInt(d3.select('#map-container').height);


  }

  addAnimatedPoint(svg: any, x: number, y: number, value: number) {
    svg.append("circle")
      .attr("cx", x)
      .attr("cy", y)
      .attr("fill", "#900")
      .attr("stroke", "#999")
      .attr("opacity", 1)
      .attr("r", 0.1)
      .transition()
      .duration(1500)
      .attr("r", value) //TODO make the ending size a non-linear function of the value
      .attr("opacity", 0)
      .on("end", function () {
        d3.select(this).remove()
      });
  }

  loadJson(json: string){

  }
}
