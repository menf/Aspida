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
  data;
  user = {
    login: "",
    password: "",
    selectedWorld: ""
  };
  settings = {
    hideSubHeader: true,
    columns: {
      id: {
        title: "ID"
      },
      name: {
        title: "Nazwa"
      },
      level: {
        title: "Poziom"
      },
      maxlevel: {
        title: "Maksymalny poziom"
      },
      duration: {
        title: "Czas ulepszenia"
      },
      resources: {
        title: "Koszt ulepszenia"
      }
    }
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
        this.data = JSON.parse(val);

        this.loggedAs = "Zalogowano";
      });
  }
  logout() {
    this.loggedAs = null;
  }
  ngOnInit() {}
}
export interface Building {
  resources: string[];
  name: string;
  duration: string;
  url: string;
  upgradeUrl: string;
  level: string;
  maxLevel: string;
  id: string;
}

export interface Village {
  id: string;
  href: string;
  active: boolean;
  buildings: Building[];
  name: string;
  x: string;
  y: string;
}
