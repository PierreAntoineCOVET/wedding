<template>
  <fieldset class="card">
    <legend>{{ $t('form.forUser') }} {{ user.firstName }} {{ user.lastName }}</legend>
    <div class="form-container">
      <div class="wide-left">
        <div class="form-item">
          <div class="form-label">{{ $t('form.attendency') }}</div>
          <div class="attendency-container">
            <div v-if="!absent" class="attendency" v-for="attendency in attendencies">
              <label class="form-check-label">
                {{ $t(attendency.dayOfWeek) }}
                <input type="checkbox" class="form-check-input" :checked="attendency.confirmed"
                       @change="updateAttendency(user.id, attendency.indexOfWeek)" />
              </label>
            </div>
          </div>
          <div class="attendency-never">
            <label class="form-check-label">
              {{ $t('dayOfWeek.never') }}
              <input type="checkbox" class="form-check-input" v-model="absent"
                     @change="clearForm(user.id, $event)"/>
            </label>
          </div>
        </div>
        <div class="form-item" v-if="user.invitation[2] == '1' && form.confirmedDays[2] == '1' && !absent">
          <div class="form-label">{{ $t('form.mealChoice') }}</div>
          <div>
            <label class="meal form-check-label">
              {{ $t('form.cheeseMeal') }}
              <input type="radio" class="form-check-input" :name="user.id + ''"
                     :value="MealChoices.Cheese" v-model="form.mealChoice" />
            </label>
            <label class="meal form-check-label">
              {{ $t('form.meatMeal') }}
              <input type="radio" class="form-check-input" :name="user.id + ''"
                     :value="MealChoices.Meat" v-model="form.mealChoice" />
            </label>
          </div>
        </div>
        <div class="form-item" v-if="!absent">
          <div class="form-label">{{ $t('form.foodAllergy') }}</div>
          <input type="text" class="form-control" maxlength="300" v-model="form.foodAllergy" />
        </div>
      </div>
      <div class="wide-right">
        <div v-if="!absent" class="form-item">
          <div class="form-label">{{ $t('form.music') }}</div>
          <input type="text" class="form-control" maxlength="300" v-model="form.musicRecommendation" />
        </div>
        <div class="form-item">
          <div class="form-label">{{ $t('form.other') }}</div>
          <div><textarea class="form-control" maxlength="500" v-model="form.other"></textarea></div>
        </div>
      </div>
    </div>

    <button class="btn btn-primary" @click="save()">
      <div v-if="isSaving" class="spinner-border" role="status">
        <span class="sr-only"></span>
      </div>
      <div v-else>
        {{ $t('form.save') }}
      </div>
    </button>
  </fieldset>
</template>

<script setup lang="ts">
  const user = defineModel<User>('user', { required: true });
  const form = defineModel<Form>('form', { required: true });

  type Attendency = {
    dayOfWeek: string,
    indexOfWeek: number,
    confirmed: boolean
  };

  import { defineModel, computed, ref } from 'vue'
  import { toast } from "vue3-toastify";
  import "vue3-toastify/dist/index.css";
  import { useI18n } from "vue-i18n";

  import { type User } from '../models/User'
  import { type Form, MealChoices } from '../models/Form'
  import { FormService } from "../services/FormService"

  const formService = new FormService();
  const isSaving = ref<boolean>(false);
  const { t } = useI18n({ useScope: "global" });
  const absent = ref<boolean>(form.value.confirmedDays[0] === "1");

  const attendencies = computed<Attendency[]>(
    () => {
      const attendency = [];

      if (user.value.invitation[1] === "1") {
        attendency.push({
          dayOfWeek: "dayOfWeek.monday",
          indexOfWeek: 1,
          confirmed: form.value.confirmedDays[1] === "1"
        });
      }
      if (user.value.invitation[2] === "1") {
        attendency.push({
          dayOfWeek: "dayOfWeek.tuesday",
          indexOfWeek: 2,
          confirmed: form.value.confirmedDays[2] === "1"
        });
      }
      if (user.value.invitation[3] === "1") {
        attendency.push({
          dayOfWeek: "dayOfWeek.wednesday",
          indexOfWeek: 3,
          confirmed: form.value.confirmedDays[3] === "1"
        });
      }
      if (user.value.invitation[4] === "1") {
        attendency.push({
          dayOfWeek: "dayOfWeek.thursday",
          indexOfWeek: 4,
          confirmed: form.value.confirmedDays[4] === "1"
        });
      }
      if (user.value.invitation[5] === "1") {
        attendency.push({
          dayOfWeek: "dayOfWeek.friday",
          indexOfWeek: 5,
          confirmed: form.value.confirmedDays[5] === "1"
        });
      }

      return attendency;
    });

  function updateAttendency(userId: number, indexOfWeek: number) {
    const currentValue = form.value.confirmedDays[indexOfWeek];
    form.value.confirmedDays = form.value.confirmedDays.substring(0, indexOfWeek)
      + (currentValue == '0' ? '1' : '0')
      + form.value.confirmedDays.substring(indexOfWeek + 1);
  }

  function clearForm(userId: number, event: Event) {
    if (!event) {
      return;
    }

    const target = event.target as HTMLInputElement;
    if (!target.checked) {
      return;
    }

    form.value.confirmedDays = '100000';
    form.value.mealChoice = 0;
    form.value.foodAllergy = '';
    form.value.musicRecommendation = '';
  }

  async function save() {
    isSaving.value = true;

    const success = form.value.id !== 0
      ? await formService.update(form.value)
      : await formService.create(form.value);

    if (success) {
      if (form.value.id === 0) {
        form.value.id = success as number;
      }

      toast(t('form.saveSuccess'), {
        "theme": "colored",
        "type": "success",
        "position": "top-center",
        "dangerouslyHTMLString": true
      });
    }
    else {
      toast(t('form.saveError'), {
        "autoClose": false,
        "theme": "colored",
        "type": "error",
        "position": "top-center",
        "hideProgressBar": true,
        "dangerouslyHTMLString": true
      });
    }

    isSaving.value = false;
  }
</script>

<style scoped>
  .form-item {
    margin: 10px 0 10px 0;
  }

  .attendency, .meal {
    margin-right: 1rem;
  }

  .form-label {
    font-weight: 500;
  }

  .card {
    padding: .5rem;
  }

  .btn,
  .btn:hover {
    color: #fff;
  }

  .form-container {
      margin-bottom: 1rem;
  }

  .attendency-never {
      margin-top: .5rem;
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

    .wide-left, .wide-right {
        flex-basis: 40%;
    }

    .form-container {
      display: flex;
      flex-direction: row;
      justify-content: space-around;
    }

    textarea {
      width: 500px;
      height: 150px;
      resize: both;
    }
  }
</style>
