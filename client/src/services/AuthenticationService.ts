
import axios from 'axios';

import { type User } from '../models/User'

export class AuthenticationService {
  static localeStorageKey: string = "authUser"

  async authenticate(userCode: string): Promise<User | null> {

    try {
      const url = `${import.meta.env.VITE_HOST_URI}user/${userCode}`;
      const response = await axios.get<User>(url);

      if (response.status == 200) {
        return response.data;
      }
    }
    catch (error) {
      console.error(error);
    }

    return null;
  }
}
