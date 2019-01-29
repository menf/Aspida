import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable, of } from "rxjs";
import { map, catchError, tap } from "rxjs/operators";
import { RequestOptions } from "@angular/http";
import { HttpParamsOptions } from "@angular/common/http/src/params";
import { environment } from "./../environments/environment";
const endpoint = "http://localhost:9000/";

@Injectable({
  providedIn: "root"
})
export class RestclientService {
  headers = new HttpHeaders()
    .set("Content-Type", "application/json")
    .set(
      "User-Agent",
      "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36"
    );
  params: any = {
    ft: "a4",
    user: "menfis",
    pw: "123123",
    s2: "Go",
    pw_servertime: "5c4b5a8c112d5"
  };
  httpParams: HttpParamsOptions = {
    fromObject: this.params
  } as HttpParamsOptions;

  options = { params: new HttpParams(this.httpParams), headers: this.headers };

  constructor(private http: HttpClient) {}

  private extractData(res: Response) {
    let body = res;
    return body || {};
  }
  connect(login, password, world): Observable<any> {
    return this.http
      .get(
        endpoint +
          "login?login=" +
          login +
          "&password=" +
          password +
          "&world=" +
          world
      )
      .pipe(map(this.extractData));
  }

  refresh(): Observable<any> {
    return this.http.get(endpoint + "refresh").pipe(map(this.extractData));
  }

  sendResources(body): Observable<any> {
    return this.http.post(endpoint + "sendResources", body);
  }
  sendTroops(body, id): Observable<any> {
    return this.http.post(endpoint + "sendTroops?v=" + id, body);
  }
}
