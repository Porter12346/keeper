<script setup>
import { AppState } from '@/AppState.js';
import { computed } from 'vue';

const keep = computed(() => AppState.activeKeep)
</script>


<template>
    <div class="modal modal-xl fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
        <div v-if="keep" class="modal-dialog">
            <div class="modal-content">
                <div class="container">
                    <div class="row">
                        <div class="col-6 p-0">
                            <img class="img-fluid w-100 rounded-start" :src="keep.img" :alt="keep.name">
                        </div>
                        <div class="col-6 p-0 d-flex flex-column justify-content-between px-4">
                            <div>
                                <p class="fs-4 text-center d-flex justify-content-center gap-4"><span
                                        class="mdi mdi-eye">{{ keep.views }}</span><span
                                        class="mdi mdi-safe-square-outline">{{ keep.kept }}</span></p>
                            </div>
                            <div>
                                <h1 class="text-center">
                                    {{ keep.name }}
                                </h1>
                                <p>
                                    {{ keep.description }}
                                </p>
                            </div>
                            <div class="d-flex justify-content-around align-items-center pb-2">
                                <h5 class="mb-0">tbd</h5>
                                <button class="btn btn-secondary">save</button>
                                <div class="d-flex align-items-center">
                                    <img class="profile-pic ms-5 px-2" :src="keep.creator.picture" :alt="keep.creator.name">
                                    <p class="mb-0">{{ keep.creator.name }}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.profile-pic{
    height: 5vh;
    border-radius: 50%;
}</style>