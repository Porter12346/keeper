<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import { keepsService } from '@/services/KeepsService.js';
import { computed, onMounted } from 'vue';

onMounted(() => getKeeps())

const keeps = computed(() => AppState.keeps)

function getKeeps() {
  keepsService.getKeeps();
}
</script>

<template>
  <div class="container">
    <div class="row">
      <div class="col-12">
        <div v-if="keeps" class="grid pb-3 pt-2">
          <div class="item my-4 mx-1" v-for="keep in keeps" :key="keep.id">
            <KeepCard :keep-prop="keep" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
img {
  width: 100%;
}

.grid {
  columns: 10rem 4;
  column-gap: 1rem;
}

// .item {
//   break-inside: avoid;
//   padding: 0.5rem;
//   border-radius: 0.75rem;
//   height: 100%;
// }</style>
