<template>
  <h3>{{ t('notfound') }}</h3>
</template>

<script setup lang="ts">
  import { useI18n } from 'vue-i18n'

  const { t } = useI18n();
</script>

<style scoped>
</style>
