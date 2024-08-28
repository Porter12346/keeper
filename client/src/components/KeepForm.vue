<script setup>
import { keepsService } from '@/services/KeepsService.js';
import Pop from '@/utils/Pop.js';
import { ref } from 'vue';


const editableKeepData = ref({
    name: "",
    img: "",
    description: ""
})

async function createKeep() {
    try {
        await keepsService.createKeep(editableKeepData.value)
        Pop.success("keep created")
        editableKeepData.value = {
            name: "",
            img: "",
            description: ""
        }
    }
    catch (error) {
        Pop.error(error);
    }

}

</script>


<template>
    <div class="modal fade" id="keepForm" tabindex="-1" aria-labelledby="keepFormLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5 fw-bold text-body-secondary" id="keepFormLabel">Add your keep</h1>
                    <button type="button" class="btn-close" title="close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createKeep()">
                        <div class="mb-3">
                            <input v-model="editableKeepData.name" placeholder="Title..." type="string"
                                class="form-control border-0 border-bottom rounded-0" id="KeepFormTitle"
                                aria-describedby="TitleHelp" required minlength="1" maxlength="255">
                        </div>
                        <div class="mb-3 pb-5">
                            <input v-model="editableKeepData.img" placeholder="Image URL..." type="URL"
                                class="form-control border-0 border-bottom rounded-0" id="KeepFormImageURL" required
                                minlength="1" maxlength="1000">
                        </div>
                        <div class="mb-3 pt-5">
                            <textarea v-model="editableKeepData.description" placeholder="Keep Description..."
                                type="string" class="form-control border-0 border-bottom rounded-0"
                                id="KeepFormImageURL" rows="1" required minlength="1" maxlength="1000"></textarea>
                        </div>
                        <div class="text-end">
                            <button type="submit" class="btn btn-dark ">Create</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

</template>


<style lang="scss" scoped></style>