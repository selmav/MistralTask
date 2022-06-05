import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse, HttpStatusCode } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { catchError, map, Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { EMPTY } from "rxjs";

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
    constructor(private readonly toastService: ToastrService) { }

    intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const request = httpRequest.clone({ url: `${environment.apiUrl}/${httpRequest.url}` });
        const isUpdate = request.method === 'POST' || request.method === 'PUT';
        const isDelete = request.method === 'DELETE';
        return next.handle(request).pipe(
            map((event: HttpEvent<any>) => {
                if (event instanceof HttpResponse && event.status === HttpStatusCode.Ok) {
                    return this.handleOkResult(event, isUpdate, isDelete);
                }

                return event;
            }),
            catchError((error: HttpErrorResponse) => {
                this.toastService.error(this.getErrorMessage(error.status), "Something went wrong.");
                return EMPTY;
            })
        );
    }

    private handleOkResult(event: HttpResponse<any>, isUpdate: boolean, isDelete: boolean) {
        if (isUpdate || isDelete) {
            this.toastService.success(`Successfuly ${isUpdate ? 'saved' : 'deleted'} item.`, "Success!")
        }

        const body = (event.body as any).result;
        return event.clone({ body });
    }

    private getErrorMessage(status: number) {
        if (status === 400) {
            return 'Please check the provided data.'
        }

        if (status === 404) {
            return 'The requested item was not found.'
        }

        return 'Internal Server Error, please try again later.'
    }
}