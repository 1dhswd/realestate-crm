export interface PropertyImage {
  id: number;
  propertyId: number;
  imageUrl: string;
  fileName: string;
  fileSize: number;
  isPrimary: boolean;
  displayOrder: number;
  createdAt: Date;
}

export interface PropertyImageUploadResponse {
  message: string;
  images: PropertyImage[];
}
