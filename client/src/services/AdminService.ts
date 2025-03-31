
import axios from 'axios';

import type { Admin } from '@/models/Admin';

export class AdminService {
  async getDatas(): Promise<Admin | null> {

    try {
      const url = `${import.meta.env.VITE_HOST_URI}admin/`;
      const response = await axios.get<Admin>(url);
      
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
