<template>
  <div v-if="loggedUser">
    <div v-if="isLoading" class="spinner-border" role="status">
      <span class="sr-only"></span>
    </div>
    <div v-else>
      <div v-for="form in forms">
        <FormComponent v-model:user="form[0]" v-model:form="form[1]" class="form" />
      </div>
      <Autocomplete />
    </div>
  </div>
  <div v-else>
    {{ $t('global.mustBeLoggedIn') }}
  </div>
</template>

<script setup lang="ts">
  import { ref, onMounted, inject } from 'vue';

  import { type User } from '../models/User'
  import { type Form, MealChoices } from '../models/Form'
  import { AuthenticationService } from '../services/AuthenticationService'
  import { FormService } from "../services/FormService"

  import Autocomplete from "./AutocompleteComponent.vue"
  import FormComponent from "./FormComponent.vue"

  const loggedUser = ref<User | null>(null);
  const eventBus = inject('eventBus') as any;
  const isLoading = ref<boolean>(true);
  const forms = ref<[User, Form][]>([]);
  const formService = new FormService();

  onMounted(async () => {
    const loggedUserString = localStorage.getItem(AuthenticationService.localeStorageKey);
    if (loggedUserString) {
      const user = JSON.parse(loggedUserString);
      loggedUser.value = user;

      const formData = await getUserForm(user);
      forms.value.push(formData);
    }

    eventBus.on('loggin', async (user: User | null) => {
      loggedUser.value = user;

      if (user) {
        const formData = await getUserForm(user);
        forms.value.push(formData);
      }
      else {
        forms.value = [];
      }
    });
  })

  async function getUserForm(user: User): [User, Form] {
    const savedForm = await formService.getForUser(user.id);

    const userForm = savedForm ?? {
        id: 0,
        userId: user.id,
        confirmedDays: "00000",
        mealChoice: MealChoices.None,
        foodAllergy: "",
        musicRecommendation: "",
        other: ""
      };

    isLoading.value = false;

    return [user, userForm];
  }
</script>

<style scoped>
  .form {
      margin-bottom: 1rem;
  }
</style>
