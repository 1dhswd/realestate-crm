export interface PropertyFilter {
  searchTerm?: string;
  minPrice?: number;
  maxPrice?: number;
  minArea?: number;
  maxArea?: number;
  city?: string;
  typeIds?: number[];
  categoryIds?: number[];
  roomCount?: number;
  bathroomCount?: number;
  isActive?: boolean;
  sortBy?: string;
  sortDirection?: 'asc' | 'desc';
  pageIndex?: number;
  pageSize?: number;
}

export interface PropertyFilterOptions {
  cities: string[];
  types: { id: number; name: string }[];
  categories: { id: number; name: string }[];
  roomOptions: number[];
  bathroomOptions: number[];
  priceRange: { min: number; max: number };
  areaRange: { min: number; max: number };
}
