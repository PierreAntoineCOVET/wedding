export enum AccessType {
  partial,
  full
}

export type User = {
  id: number,
  name: string,
  accessGroup: AccessType
}
