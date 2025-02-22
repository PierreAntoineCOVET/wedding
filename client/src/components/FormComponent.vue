<template>
  <div v-if="loggedUser">
    <div v-if="isLoading" class="spinner-border" role="status">
      <span class="sr-only"></span>
    </div>
    <div v-else>
      <div v-for="form in forms">
        <fieldset>
          <legend>{{ $t('form.forUser') }} {{ form[0].firstName }} {{ form[0].lastName }}</legend>
          <div class="formItem">
            {{ $t('form.attendency') }}
            <div class="attendency-container">
              <div class="attendency" v-for="attendency in computeAttendency(form[0].invitation, form[1].confirmedDays)">
                <label>
                  {{ $t(attendency.dayOfWeek) }}
                  <input type="checkbox" :checked="attendency.confirmed" @change="updateAttendency(form[0].id, attendency.indexOfWeek)" />
                </label>
              </div>
            </div>
          </div>
          <div class="formItem" v-if="form[0].invitation[3] == 1">
            {{ $t('form.mealChoice') }}
            <div>
              <label class="meal">
                {{ $t('form.cheeseMeal') }}
                <input type="radio" name="form[0].id" />
              </label>
              <label class="meal">
                {{ $t('form.meatMeal') }}
                <input type="radio" name="form[0].id" />
              </label>
            </div>
          </div>
          <div class="formItem">{{ $t('form.foodAllergy') }} <input type="text" /></div>
          <div class="formItem">{{ $t('form.music') }} <input type="text" /></div>
          <div class="formItem">{{ $t('form.other') }} <div><textarea></textarea></div></div>
        </fieldset>
      </div>
      <Autocomplete />
    </div>
  </div>
  <div v-else>
    {{ $t('global.mustBeLoggedIn') }}
  </div>
</template>

<script setup lang="ts">
  type Attendency = {
    dayOfWeek: string,
    indexOfWeek: number,
    confirmed: boolean
  };

  import { ref, onMounted, inject } from 'vue';

  import { type User } from '../models/User'
  import { type Form, MealChoices } from '../models/Form'
  import { AuthenticationService } from '../services/AuthenticationService'
  import { FormService } from "../services/FormService"

  import Autocomplete from "./AutocompleteComponent.vue"

  const loggedUser = ref<User | null>(null);
  const eventBus = inject('eventBus') as any;
  const isLoading = ref<boolean>(true);
  const forms = ref<[User, Form][]>([]);
  const formService = new FormService();

  onMounted(() => {
    const loggedUserString = localStorage.getItem(AuthenticationService.localeStorageKey);
    if (loggedUserString) {
      loggedUser.value = JSON.parse(loggedUserString);

      getUserForm(loggedUser.value);
    }

    eventBus.on('loggin', (user: User | null) => {
      loggedUser.value = user;

      getUserForm(loggedUser.value);
    });
  })

  async function getUserForm(user: User) {
    const savedForm = await formService.getForUser(user.id)

    if (savedForm == null) {
      const newForm: Form = {
        userId: user.id,
        confirmedDays: "00000",
        mealChoice: MealChoices.None,
        foodAllergy: "",
        musicRecommendation: "",
        other: ""
      }

      forms.value.push([user, newForm]);
    }

    isLoading.value = false;
  }

  function computeAttendency(invitedDays: string, confirmedDays: string): Attendency[] {
    const attendency = [];

    if (invitedDays[0] === "1") {
      attendency.push({
        dayOfWeek: "dayOfWeek.monday",
        indexOfWeek: 0,
        confirmed: confirmedDays[0] === "1"
      });
    }
    if (invitedDays[1] === "1") {
      attendency.push({
        dayOfWeek: "dayOfWeek.tuesday",
        indexOfWeek: 1,
        confirmed: confirmedDays[1] === "1"
      });
    }
    if (invitedDays[2] === "1") {
      attendency.push({
        dayOfWeek: "dayOfWeek.wednesday",
        indexOfWeek: 2,
        confirmed: confirmedDays[2] === "1"
      });
    }
    if (invitedDays[3] === "1") {
      attendency.push({
        dayOfWeek: "dayOfWeek.thursday",
        indexOfWeek: 3,
        confirmed: confirmedDays[3] === "1"
      });
    }
    if (invitedDays[4] === "1") {
      attendency.push({
        dayOfWeek: "dayOfWeek.friday",
        indexOfWeek: 4,
        confirmed: confirmedDays[4] === "1"
      });
    }

    return attendency;
  }

  function updateAttendency(userId: number, indexOfWeek: number) {
    const form = forms.value.find(e => e[0].id === userId);
    const days = form[1].confirmedDays;
    const currentValue = days[indexOfWeek];
    form[1].confirmedDays = days.substring(0, indexOfWeek) + (currentValue == '0' ? '1' : '0') + days.substring(indexOfWeek + 1);
  }
</script>

<style scoped>
  .formItem {
    margin: 10px 0 10px 0;
  }

  .attendency, .meal {
    margin-right: 1rem;
  }

  @media (max-width: 1024px) {
    textarea {
      width: 300px;
      height: 150px;
      resize: both;
    }
  }

  @media (min-width: 1024px) {
    .attendency-container {
      display: flex;
      flex-direction: row;
    }

    textarea {
      width: 500px;
      height: 150px;
      resize: both;
    }
  }
</style>
