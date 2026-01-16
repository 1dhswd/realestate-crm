export interface Property {
  id: number;
  title: string;
  description?: string;
  price: number;
  area: number;
  roomCount?: number;
  bathroomCount?: number;
  floorNumber?: number;
  buildingAge?: number;
  address: string;
  city: string;
  district?: string;
  latitude?: number;
  longitude?: number;
  isActive: boolean;
  isFeatured: boolean;
  viewCount: number;
  publishedAt?: Date;
  createdAt: Date;
  updatedAt: Date;

  categoryName?: string;
  typeName?: string;
  ownerName?: string;
  features?: string[];
  imageUrls?: string[];

  categoryId?: number;
  typeId?: number;
  ownerId?: number;
}

export interface CreatePropertyRequest {
  title: string;
  description?: string;
  price: number;
  area: number;
  roomCount?: number;
  bathroomCount?: number;
  floorNumber?: number;
  buildingAge?: number;
  address: string;
  city: string;
  district?: string;
  latitude?: number;
  longitude?: number;
  categoryId: number;
  typeId: number;
  ownerId: number;
  featureIds: number[];
  imageUrls: string[];
}

export interface UpdatePropertyRequest {
  id: number;
  title: string;
  description?: string;
  price: number;
  area: number;
  roomCount?: number;
  bathroomCount?: number;
  floorNumber?: number;
  buildingAge?: number;
  address: string;
  city: string;
  district?: string;
  latitude?: number;
  longitude?: number;
  isActive: boolean;
  isFeatured: boolean;
  categoryId: number;
  typeId: number;
  featureIds: number[];
}
