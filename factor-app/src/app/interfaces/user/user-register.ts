import { ModelBase } from "../base/model-base";

export interface UserRegister {
    registrationProvider: string;
    userName: string;
    email: string;
    password: string;
    firstName: string;
    lastName: string;
    instagramPage: string;
    storeName: string;
    storeEnglishName: string;
    storeUrl: string;
    storeDescription: string;
}
