export interface BaseResponse {
    httpStatusCode: number;
    message?: string;
    validationErrors: string[];
}