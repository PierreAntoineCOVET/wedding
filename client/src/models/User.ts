import type { Form } from "@/models/Form"

export enum Roles {
  user,
  admin
}

export type User = {
  id: number,
  userName: string,
  role: Roles,
  firstName: string,
  lastName: string,
  invitation: string,
  form: Form
}
