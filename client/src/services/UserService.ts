
import axios from 'axios';

import { type AutocompleteItem } from '../models/autocompleteItem'

export class UserService {
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
}
