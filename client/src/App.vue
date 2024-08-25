<script setup>
import Navbar from './components/Navbar.vue';
import { AppState } from './AppState.js';
import KeepModal from './components/KeepModal.vue';
import KeepForm from './components/KeepForm.vue';
import VaultForm from './components/VaultForm.vue';

</script>

<template>
  <header class="order-md-0 order-1 sticky-bottom">
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <KeepModal />
  <KeepForm />
  <VaultForm />
</template>

<style lang="scss">
@import '@/assets/scss/main.scss';

:root {
  --main-height: calc(100vh - 32px - 64px);
}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
