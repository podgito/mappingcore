import { Injectable } from '@angular/core';
import * as d3 from 'd3v4';
import * as topojson from 'topojson';

@Injectable({
  providedIn: 'root'
})
export class D3SvgService {

  constructor() { }

  createSvg(containerElement: string, width, height) {
    return d3.select("#map-container").append("svg")
      .attr("width", width)
      .attr("height", height);
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
}
