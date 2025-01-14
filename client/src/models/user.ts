export enum AccessGroup {
  tuesday,
  wednesday,
  tuesdayToThursday,
  full,
  admin
}

export type User = {
  id: number,
  name: string,
  accessGroup: AccessGroup
}
