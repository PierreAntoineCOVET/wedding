<template>
  <div v-if="loggedUser">
    <div v-if="isLoading" class="spinner-border" role="status">
      <span class="sr-only"></span>
    </div>
    <div v-else>
      <div v-for="form in forms">
        <FormComponent v-model:user="form[0]" v-model:form="form[1]" class="form" />
      </div>
      {{ $t('forms.fillForSomeoneElse') }}
      <Autocomplete :placeholder="$t('forms.searchBy')" @on-selection="addForm($event)" clear-on-select />
      <div class="filler"></div>
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
  import { UserService } from '../services/UserService'
  import { FormService } from "../services/FormService"

  import Autocomplete from "./AutocompleteComponent.vue"
  import FormComponent from "./FormComponent.vue"

  const loggedUser = ref<User | null>(null);
  const eventBus = inject('eventBus') as any;
  const isLoading = ref<boolean>(true);
  const forms = ref<[User, Form][]>([]);

  const formService = new FormService();
  const userService = new UserService();

  onMounted(async () => {
    const loggedUserString = localStorage.getItem(UserService.localeStorageKey);
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

  async function getUserForm(user: User): Promise<[User, Form]> {
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

  async function addForm(userId: number) {
    const user = await userService.getUser(userId);

    if (!user) {
      alert('Error');
      return;
    }

    const formData = await getUserForm(user);
    forms.value.push(formData);
  }
</script>

<style scoped>
  .form {
      margin-bottom: 1rem;
  }

  .filler {
      height: 400px;
  }
</style>
