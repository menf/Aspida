import {
  NbMenuModule,
  NbSidebarService,
  NbSidebarModule,
  NbLayoutModule,
  NbMenuService,
  NbUserModule
} from "@nebular/theme";
import { NgModule } from "@angular/core";

import { PagesComponent } from "./pages.component";
import { DashboardModule } from "./dashboard/dashboard.module";
import { PagesRoutingModule } from "./pages-routing.module";

const PAGES_COMPONENTS = [PagesComponent];

@NgModule({
  imports: [
    PagesRoutingModule,
    DashboardModule,
    NbLayoutModule,
    NbUserModule,
    NbSidebarModule.forRoot(),
    NbMenuModule.forRoot()
  ],
  declarations: [...PAGES_COMPONENTS],
  providers: [NbSidebarService, NbMenuService]
})
export class PagesModule {}
