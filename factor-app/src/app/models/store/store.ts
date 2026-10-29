import { ModelBase } from "../base/model-base";
import * as Interfaces from '../../interfaces/store/store';
import { ImageAsset } from "../image-asset/image-asset";

export class Store extends ModelBase implements Interfaces.Store {
    
    public storeEnglishName: string;
    public name: string;
    public ownerId: string;
    public url: string;
    public logoId: string;
    public description: string;
    public logo: ImageAsset;
    public coverImageId: string;
    public coverImage: ImageAsset;
    public tagline: string;
    public themePalette: string = 'forest';
    public isPublic: boolean = true;
    public taxPercent: number = 9;
    public socialLinks: StoreSocialLink[] = [];
}

export class StoreSocialLink extends ModelBase implements Interfaces.StoreSocialLink {
    public storeId: string;
    public platform: string = 'instagram';
    public url: string;
    public label: string;
    public isPrimary: boolean = false;
}
