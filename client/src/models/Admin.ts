import type { User } from "@/models/User"

export type Admin = {
  unAnswered: User[],
  comming: User[],
  notComming: User[]
}
