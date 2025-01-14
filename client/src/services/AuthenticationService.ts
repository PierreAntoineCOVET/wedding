
import axios from 'axios';

import { type User } from '../models/user'

export class AuthenticationService {
  static localeStorageKey: string = "authUser"

  async authenticate(userCode: string): Promise<User | null> {

    try {
      const response = await axios.get<User>(`http://localhost:5000/user/user/${userCode}`);

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
