import { ModelBase } from "../base/model-base";
import * as Interfaces from '../../interfaces/product/product';
import { Category } from "../../interfaces/category/category";
import { ImageAsset } from "../../interfaces/image-asset/image-asset";

export class Product extends ModelBase implements Interfaces.Product {
    public storeId: string;
    public name: string;
    public price: number;
    public category: Category;
    public categoryId: string;
    public imageId: string;
    public image: ImageAsset;
    public shortDescription: string;
    public showOnLanding: boolean = true;
    public sortOrder: number = 0;
}
