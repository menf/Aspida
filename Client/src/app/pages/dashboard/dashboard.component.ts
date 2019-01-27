import { Component, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { RestclientService } from "src/app/restclient.service";

@Component({
  selector: "ngx-dashboard",
  templateUrl: "./dashboard.component.html",
  styleUrls: ["./dashboard.component.scss"]
})
export class DashboardComponent implements OnInit {
  constructor(private rest: RestclientService) {}
  loggedAs: string;

  user = {
    login: "",
    password: "",
    selectedWorld: ""
  };
  sel(e) {
    console.log(e);
  }
  login() {
    this.rest
      .connect(
        this.user.login,
        this.user.password,
        this.user.selectedWorld
      )
      .subscribe(val => {
        console.log(val);
        this.loggedAs = val;
      });
  }
  logout() {
    this.loggedAs = null;
  }
  ngOnInit() {}
}
