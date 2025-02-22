<template>
  <div v-if="loggedUser">
    <p>Bon alors qu’est ce qui va se passer et comment ça va se passer ?</p>

    <p>Tout d’abord, attention à la date ! Nous avons décidé de nous marier mais en SEMAINE ! (oui on aime bien faire les choses différemment).</p>

    <Tuesday v-if="invitationPattern('01000')" />

    <Wednesday v-if="invitationPattern('00100')" :isMulti="invitationPattern('01100')" />

    <Thursday v-if="invitationPattern('00010')" :isMulti="invitationPattern('00110')" />

    <Friday v-if="invitationPattern('00001')" :isMulti="invitationPattern('00011')" />

    <p>
      Notre lieu de réception, ne nous permets pas de recevoir tout le monde en même temps.
      Certains vont venir de très loin, ils seront présent sur plusieurs jours et hébergés sur place. Pour les
      locaux, nous vous invitons à revenir partager la journée détente du jeudi si vous êtes disponibles !
      (merci de le préciser dans votre confirmation de présence).
    </p>
  </div>
  <div v-else>
    {{ $t('global.mustBeLoggedIn') }}
  </div>
</template>

<script setup lang="ts">
  import { ref, onMounted, inject } from 'vue';

  import { type User } from '../models/User'
  import { AuthenticationService } from '../services/AuthenticationService';

  import Tuesday from './ScheduleTuesdayComponent.vue'
  import Wednesday from './ScheduleWednesdayComponent.vue'
  import Thursday from './ScheduleThursdayComponent.vue'
  import Friday from './ScheduleFridayComponent.vue'

  const loggedUser = ref<User | null>(null);
  const eventBus = inject('eventBus') as any;

  onMounted(() => {
    const loggedUserString = localStorage.getItem(AuthenticationService.localeStorageKey);
    if (loggedUserString) {
      loggedUser.value = JSON.parse(loggedUserString);
    }

    eventBus.on('loggin', (user: User | null) => {
      loggedUser.value = user;
    });
  })

  function invitationPattern(pattern: string): boolean {
    if (!loggedUser.value) {
      return false;
    }

    const userMask = parseInt(loggedUser.value.invitation, 2);
    const patternMask = parseInt(pattern, 2);

    return (userMask & patternMask) == patternMask;
  }

</script>

<style scoped>
</style>
