
import axios, { type AxiosRequestConfig } from 'axios';

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

  async create(form: Form): Promise<boolean> {

    try {
      const url = `${import.meta.env.VITE_HOST_URI}form`;
      const config: AxiosRequestConfig = {
        headers: { 'Content-Type': 'application/json' }
      };
      const response = await axios.post<Form>(url, form, config);

      if (response.status == 201) {
        return true;
      }
    }
    catch (error) {
      console.error(error);
    }

    return false;
  }

  async update(form: Form): Promise<boolean> {

    try {
      const url = `${import.meta.env.VITE_HOST_URI}form`;
      const config: AxiosRequestConfig = {
        headers: { 'Content-Type': 'application/json' }
      };
      const response = await axios.put<Form>(url, form, config);

      if (response.status == 200) {
        return true;
      }
    }
    catch (error) {
      console.error(error);
    }

    return false;
  }
}
