
import axios from 'axios';

import { type User } from '../models/user'

export class UserService {
  async search(userQuery: string): Promise<User[]> {

    try {
      const url = `${import.meta.env.VITE_HOST_URI}user/search/${userQuery}`;
      const response = await axios.get<User[]>(url);

      if (response.status == 200) {
        return response.data;
      }
    }
    catch (error) {
      console.error(error);
    }

    return [];
  }
}
