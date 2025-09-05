export interface Employee {
  id: number;
  name: string;
  positionId?: number;
  positionName?: string;
  birthDate?: Date;
  isActive: boolean;
  startDate: Date;
  phone?: string;
  managerId?: any | null;
  managerName?: string | null;
  departmentId?: number;
  departmentName?: string;
  image?: any;
}