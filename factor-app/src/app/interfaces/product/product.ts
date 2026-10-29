import * as Interfaces from '../user/user';
import { ModelBase } from '../base/model-base';
import { Category } from '../category/category';
import { ImageAsset } from '../image-asset/image-asset';

export interface Product extends ModelBase {
    storeId: string;
    name:string;
    price:number; 
    category: Category;
    categoryId:string;
    imageId: string;
    image: ImageAsset;
    shortDescription: string;
    showOnLanding: boolean;
    sortOrder: number;
}

