import { Injectable, ɵConsole } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


declare var jQuery:any;

@Injectable({
  providedIn: 'root'
})


export class HttpService {

  address = 'http://localhost:5000';

  constructor(private httpClient: HttpClient) {

  }





  async get(url: string) {
    try {
      var result = await this.httpClient.get(this.address + url,
      {
        headers:
          new HttpHeaders(
            {
              'Content-Type': 'application/json',
              'X-Requested-With': 'XMLHttpRequest',
              'MyClientCert': '',        // This is empty
              'MyToken': ''              // This is empty
            }
          )
      }
      ).toPromise();
      return result;
    } catch (error) {
      console.error(error);
      throw error;
    }
    finally {
    }
  }

  async post(url: string, data:any) {
    try {
      var result = await this.httpClient.post<any>(this.address + url, data).toPromise();
      return result;
    } catch (error) {
      console.error(error);
      throw error;
    }
    finally {
    }

  }

  async delete(url: string) {
    try {
      var result = await this.httpClient.delete<any>(this.address + url).toPromise();
      return result;
    } catch (error) {
      console.error(error);
      throw error;
    }
    finally {
    }
  }

}
