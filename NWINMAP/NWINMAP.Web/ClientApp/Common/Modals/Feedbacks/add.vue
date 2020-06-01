<style>
</style>
<template>
    <b-modal ref="modal"
             size="lg"
             scrollable
             :no-close-on-esc="true"
             :no-close-on-backdrop="true"
             :header-class="`p-2`"
             :body-class="`p-2`"
             :footer-class="`p-2`">
        <template v-slot:modal-header>
            <div class="d-sm-flex align-items-center justify-content-between">
                <h1 class="h2 m-0 text-gray-800"><span class="fas fa-fw fa-comment-alt"></span>&nbsp;Your Feedback</h1>
            </div>
        </template>
        <template v-slot:modal-footer>
            <button @click="save" v-bind:disabled="!isFormValid" class="btn btn-primary">
                Send
            </button>
            <button @click="close" class="btn btn-secondary ml-2">
                Ask Me Later
            </button>
        </template>
        <div>
            <b-alert :show="errorMessage!==null" dismissible variant="warning">
                <div v-html="errorMessage"></div>
            </b-alert>

            <div v-if="message" v-text="message" class="alert alert-info">
            </div>
            <div class="form-group">
                <label class="col col-form-label">
                    Rate your experience when using our application.
                </label>
                <div class="d-flex flex-row justify-content-around align-items-center mt-4">
                    <span v-bind:class="{'fa-4x bg-gradient-light border rounded p-1': item.rating===1}" @click="item.rating = 1" class="fas fa-3x fa-frown-open"></span>
                    <span v-bind:class="{'fa-4x bg-gradient-light border rounded p-1': item.rating===2}" @click="item.rating = 2" class="fas fa-3x fa-frown"></span>
                    <span v-bind:class="{'fa-4x bg-gradient-light border rounded p-1': item.rating===3}" @click="item.rating = 3" class="fas fa-3x fa-meh"></span>
                    <span v-bind:class="{'fa-4x bg-gradient-light border rounded p-1': item.rating===4}" @click="item.rating = 4" class="fas fa-3x fa-smile"></span>
                    <span v-bind:class="{'fa-4x bg-gradient-light border rounded p-1': item.rating===5}" @click="item.rating = 5" class="fas fa-3x fa-laugh"></span>
                </div>
            </div>
           
            <div class="form-group">
                <hr />
                <label class="col col-form-label">
                    Please select your feedback category below.
                </label>
                <div class="col">
                    <div class="d-flex flex-column flex-sm-row justify-content-around">
                        <div class="custom-control custom-radio">
                            <input type="radio" id="customRadio1" name="customRadio" class="custom-control-input" value="1" v-model="item.feedbackCategory">
                            <label class="custom-control-label" for="customRadio1">Suggestion</label>
                        </div>
                        <div class="custom-control custom-radio">
                            <input type="radio" id="customRadio2" name="customRadio" class="custom-control-input" value="2" v-model="item.feedbackCategory">
                            <label class="custom-control-label" for="customRadio2">Something is not quite right</label>
                        </div>
                        <div class="custom-control custom-radio">
                            <input type="radio" id="customRadio3" name="customRadio" class="custom-control-input" value="3" v-model="item.feedbackCategory">
                            <label class="custom-control-label" for="customRadio3">Compliment</label>
                        </div>

                    </div>
                </div>

            </div>
           
            <div class="form-group">
                <hr />
                <label class="col col-form-label">
                    Please leave your feedback below.
                </label>
                <div class="col">
                    <textarea v-model="item.comment" rows="3" class="form-control"></textarea>
                </div>
            </div>
        </div>
    </b-modal>
</template>
<script>
    //import commonMixin from '../../commonMixin';

    export default {
        //mixins:[commonMixin],

        data() {
            return {
                warningMessage: null,
                errorMessage: null,

                message: null,

                item: {
                    feedbackCategory: null,
                    rating: null,
                    comment: null
                },
                itemClone: {}
            };
        },
        computed: {

            isFormValid() {
                let vm = this;
                let item = vm.item;

                if (!item.feedbackCategory)
                    return false;
                if (!item.rating)
                    return false;
                if (!item.comment)
                    return false;

                return true;
            }
        },
        async mounted() {
            this.itemClone = this.$util.clone(this.item);
        },
        methods: {
            reset() {
                let vm = this;
                vm.message = null;
                vm.item = vm.$util.clone(vm.itemClone);
            },
            async open(message) {
                let vm = this;
                vm.reset();
                vm.message = message;

                vm.$refs.modal.show();
            },
            close() {
                let vm = this;
                vm.$refs.modal.hide();
                vm.$emit('closed');

                const now = moment().add(1, 'days').format();

                localStorage.setItem('nextFeedbackRequestDate', now);
            },
            async save() {
                let vm = this;

                let payload = vm.clone(vm.item);

                try {
                    vm.errorMessage = null;
                    await vm.$util.axios.post(`api/feedbacks`, payload)
                        .then(resp => {
                            vm.$bvToast.toast('Feedback sent. Thank you.', { title: 'Send Feedback', variant: 'success' });
                            vm.$refs.modal.hide();

                            vm.$bus.$emit('event:feedback-created', resp.data);
                        })
                        ;
                } catch (e) {
                    vm.errorMessage = vm.$util.getErrorMessageInHtml(e);
                }
            }
        }
    }
</script>
