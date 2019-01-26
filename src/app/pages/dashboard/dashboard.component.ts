import { Component, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { RestclientService } from "src/app/restclient.service";

@Component({
  selector: "ngx-dashboard",
  templateUrl: "./dashboard.component.html",
  styleUrls: ["./dashboard.component.scss"]
})
export class DashboardComponent implements OnInit {
  constructor(private rest: RestclientService) {}

  ngOnInit() {}
}
