export enum AccessGroup {
  tuesday,
  wednesday,
  tuesdayToThursday,
  full,
  admin
}

export type User = {
  id: number,
  userName: string,
  accessGroup: AccessGroup
}
