import { ModelBase } from "../base/model-base";
import { ImageAsset } from "../image-asset/image-asset";

export interface Store extends ModelBase {
    storeEnglishName: string;
    name: string;
    ownerId: string;
    url: string;
    logoId: string;
    description: string;
    logo:ImageAsset;
    coverImageId: string;
    coverImage: ImageAsset;
    tagline: string;
    themePalette: string;
    isPublic: boolean;
    socialLinks: StoreSocialLink[];
}

export interface StoreSocialLink extends ModelBase {
    storeId: string;
    platform: string;
    url: string;
    label: string;
    isPrimary: boolean;
}
