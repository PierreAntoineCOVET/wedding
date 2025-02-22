
import axios from 'axios';

import { type Form } from '../models/Form'

export class FormService {
  async getForUser(userId: number): Promise<Form | null> {

    try {
      const url = `${import.meta.env.VITE_HOST_URI}form/foruser/${userId}`;
      const response = await axios.get<Form>(url);

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
