
import axios from 'axios';

import { type AutocompleteItem } from '../models/AutocompleteItem';
import { type User } from '../models/User';

export class UserService {
  static localeStorageKey: string = "authUser"

  async authenticate(userCode: string): Promise<User | null> {

    try {
      const url = `${import.meta.env.VITE_HOST_URI}user?userName=${userCode}`;
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

  async search(userQuery: string): Promise<AutocompleteItem[]> {

    try {
      const url = `${import.meta.env.VITE_HOST_URI}user/search?query=${userQuery}`;
      const response = await axios.get<AutocompleteItem[]>(url);

      if (response.status == 200) {
        return response.data;
      }
    }
    catch (error) {
      console.error(error);
    }

    return [];
  }

  async getUser(userId: number): Promise<User | null> {

    try {
      const url = `${import.meta.env.VITE_HOST_URI}user/${userId}`;
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
