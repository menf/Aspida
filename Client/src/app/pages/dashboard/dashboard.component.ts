import {
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
  ElementRef
} from "@angular/core";
import { RestclientService } from "src/app/restclient.service";
import { interval } from "rxjs";
import { flatMap } from "rxjs/operators";

@Component({
  selector: "ngx-dashboard",
  templateUrl: "./dashboard.component.html",
  styleUrls: ["./dashboard.component.scss"]
})
export class DashboardComponent implements OnInit, OnDestroy {
  ngOnDestroy(): void {
    clearInterval(this.intervalId);
  }
  constructor(private rest: RestclientService) {}
  loggedAs: string;
  data;
  messages = [];
  messages2 = [];
  intervalId;
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
        console.log(this.data);
        this.data.messages.forEach(element => {
          this.messages.push(element);
        });
        this.messages2 = null;
        this.messages2 = this.messages;
        this.loggedAs = "Zalogowano";
        this.intervalId = interval(15 * 1000)
          .pipe(flatMap(() => this.rest.refresh()))
          .subscribe(data => console.log(data));
      });
  }
  logout() {
    this.loggedAs = null;
    this.data = null;
    clearInterval(this.intervalId);
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
