import * as Interfaces from '../../interfaces/user/user-register';

export class UserRegister implements Interfaces.UserRegister {
    public registrationProvider: string = 'email';
    public userName: string = '';
    public email: string = '';
    public password: string = '';
    public firstName: string = '';
    public lastName: string = '';
    public instagramPage: string = '';
    public storeName: string = '';
    public storeEnglishName: string = '';
    public storeUrl: string = '';
    public storeDescription: string = '';
}
