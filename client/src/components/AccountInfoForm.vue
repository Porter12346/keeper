<script setup>
import { Account } from '@/models/Account.js';
import { accountService } from '@/services/AccountService.js';
import { Modal } from 'bootstrap';
import { ref } from 'vue';

const props = defineProps({account: {type: Account, required: true}})

const editableAccountData = ref({name: props.account.name, picture: props.account.picture, coverImg: props.account.coverImg})

async function editAccountData() {
    await accountService.editAccountData(editableAccountData.value)
    Modal.getOrCreateInstance("#AccountInfo").hide()
}
</script>


<template>
    <div class="modal fade" id="AccountInfo" tabindex="-1" aria-labelledby="AccountInfoLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="AccountInfoLabel">Edit Account</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="editAccountData()">
                        <div class="mb-3">
                            <label for="nameInput" class="form-label">Name</label>
                            <input v-model="editableAccountData.name" type="string" class="form-control" id="nameInput">
                        </div>
                        <div class="mb-3">
                            <label for="profilePicInput" class="form-label">Profile picture</label>
                            <input v-model="editableAccountData.picture" type="URL" class="form-control" id="ProfilePicInput">
                        </div>
                        <div class="mb-3">
                            <label for="coverImgInput" class="form-label">cover Img</label>
                            <input v-model="editableAccountData.coverImg" type="URL" class="form-control" id="coverImgInput">
                        </div>
                        <div class="d-flex justify-content-end gap-3">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="submit" class="btn btn-primary">Save changes</button>
                    </div>
                    </form>
                </div>

            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped></style>