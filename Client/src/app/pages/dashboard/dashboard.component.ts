import {
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
  ElementRef,
  ChangeDetectorRef,
  ChangeDetectionStrategy
} from "@angular/core";
import { RestclientService } from "src/app/restclient.service";
import { interval } from "rxjs";
import { flatMap } from "rxjs/operators";

@Component({
  selector: "ngx-dashboard",
  templateUrl: "./dashboard.component.html",
  styleUrls: ["./dashboard.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardComponent implements OnInit, OnDestroy {
  ngOnDestroy(): void {
    clearInterval(this.intervalId);
  }
  constructor(private rest: RestclientService, private cd: ChangeDetectorRef) {}
  loggedAs: string;
  data;
  troops = [];
  messages = [];
  intervalId;
  user = {
    login: "",
    password: "",
    selectedWorld: ""
  };
  surowce = {
    drewno: "",
    glina: "",
    zelazo: "",
    zboze: "",
    x: "",
    y: ""
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
      maxLevel: {
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
  settingsunits = {
    hideSubHeader: true,
    columns: {
      id: {
        title: "ID"
      },
      name: {
        title: "Nazwa"
      },
      count: {
        title: "Ilość"
      }
    }
  };
  sel(e) {
    console.log(e);
  }
  sendT(val, id) {
    console.log(val);
    this.rest.sendTroops(val, id).subscribe(res => {
      console.log(res);
    });
  }
  sendR(val, villageid) {
    var id = this.getMarket(val);
    this.rest
      .sendResources({
        villageid: villageid,
        id: id,
        r1: this.surowce.drewno,
        r2: this.surowce.glina,
        r3: this.surowce.zelazo,
        r4: this.surowce.zboze,
        x: this.surowce.x,
        y: this.surowce.y
      })
      .subscribe(res => {
        console.log(res);
      });
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
        this.messages = [...this.messages, ...this.data.messages];
        this.cd.markForCheck();

        this.loggedAs = "Zalogowano";
      });
  }
  refresh() {
    this.rest.refresh().subscribe(data => {
      this.data = JSON.parse(data);
      this.messages = [...this.messages, ...this.data.messages];
      this.cd.markForCheck();
    });
  }
  containsMarket(buildings: []) {
    var val;
    var contains = false;
    for (val of buildings) {
      if (val.name === "Marketplace") {
        contains = true;
      }
    }
    return contains;
  }
  getMarket(buildings: []) {
    var val;
    var id;
    for (val of buildings) {
      if (val.name === "Marketplace") {
        id = val.id;
      }
    }
    return id;
  }
  logout() {
    this.loggedAs = null;
    this.data = null;
    clearInterval(this.intervalId);
    this.cd.markForCheck();
  }
  // sd() {
  //  this.intervalId = interval(500 * 60 * 1000)
  //    .pipe(flatMap(() => this.rest.refresh()))
  //    .subscribe(data => {
  //      this.data = JSON.parse(data);
  //      this.messages = [...this.messages, ...this.data.messages];
  //      this.cd.markForCheck();
  //     });
  // }
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
