<style scoped>
   
</style>
<template>
    <div>
        <b-modal ref="modal"
                 size="xl"
                 :no-close-on-esc="true"
                 :no-close-on-backdrop="true"
                 hide-footer
                 @shown="scrollToBottom"
                 :body-class="`bg-light p-0 m-0`">
            <template v-slot:modal-header>
                <div class="d-sm-flex align-items-center justify-content-between">
                    <h1 class="h3 m-0 text-gray-800"><span class="fas fa-envelope"></span>&nbsp;View Chat</h1>
                </div>
            </template>
            <template v-slot:default>
                <div class="row p-0 m-0">
                    <div class="col-md-3 p-2">
                        <div class="card shadow-sm">
                            <div style="overflow-y:auto; max-height:399px;">
                                <ul class="list-group list-group-flush">
                                    <li v-for="user in item.receivers"
                                        :key="user.userId"
                                        class="list-group-item p-2">
                                        <img :src="user.urlPicture" class="img-profile rounded-circle mr-2" style="width:3rem;height:3rem;" />
                                        <span v-bind:class="{'text-danger': user.isRemoved}">{{user.owner}}</span>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-9 p-2">
                        <div class="card shadow-sm">
                            <div style="overflow-y:auto; max-height:399px;" id="messages" ref="messages">
                                <div class="list-group list-group-flush">
                                    <div v-for="msg in messages"
                                         :key="msg.chatMessageId"
                                         :id="msg.chatMessageId"
                                         class="list-group-item list-group-item-action p-2">
                                        <div class="d-flex w-100 justify-content-between">
                                            <div v-if="!msg.isOwner" class="d-flex flex-column">
                                                <img :src="msg.urlPicture" class="img-profile rounded-circle mx-auto" style="width:3rem;height:3rem;" />
                                                <small>{{msg.dateSent|moment('calendar')}}</small>
                                            </div>
                                            <p class="flex-grow-1 p-0"
                                               v-bind:class="{'pl-3': !msg.isOwner, 'pr-3': msg.isOwner}">{{msg.content}}</p>
                                            <div v-if="msg.isOwner" class="d-flex flex-column">
                                                <img :src="msg.urlPicture" class="img-profile rounded-circle mx-auto" style="width:3rem;height:3rem;" />
                                                <small>{{msg.dateSent|moment('calendar')}}</small>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="mt-3 d-flex align-content-end align-items-start">
                            <button @click="leave()" class="btn btn-warning">
                                Leave Chat
                            </button>

                            <textarea rows="3" ref="content" v-model="content" type="text" class="form-control shadow-sm ml-2"></textarea>

                            <button v-on:click="send()" class="btn btn-primary ml-3">
                                <span class="fas fa-paper-plane mr-2"></span>Send
                            </button>
                            <button @click="close_internal()" class="btn btn-secondary ml-2">
                                <span class="fas fa-times mr-2"></span>Close
                            </button>

                        </div>
                    </div>
                </div>
            </template>
            <template v-slot:modal-footer>

            </template>



        </b-modal>
    </div>
</template>
<script>
    import * as signalR from '@aspnet/signalr';

    export default {
        props: {
            uid: String
        },
        data() {
            return {
                chatHub: null,
                content: null,
                chatId: null,
                item: {},
                messages: []

            };
        },
        mounted() {

        },
        methods: {
            async onChatMessageReceived(resp) {
                let vm = this;

                if (vm.item.chatId !== resp.chatId) {
                    return;
                }

                vm.item.hasPendingMessage = true;
                vm.processMessage(resp);
                vm.messages.push(resp);

                vm.newTab();
            },

            reset() {
                let vm = this;
                vm.chatHub = null;
                vm.content = null;
                vm.chatId = null;
                vm.item = {};
                vm.messages = [];
            },
            async openChatHub() {
                let vm = this;

                vm.chatHub = new signalR.HubConnectionBuilder()
                    .withUrl('/chatHub')
                    .build();

                let start = vm.chatHub.start();

                await start.then(function () {

                    vm.chatHub.on('chatMessageReceivedFromGroup', vm.onChatMessageReceived);
                    vm.chatHub.on('chatMessageReceived', vm.onChatMessageReceived);

                    vm.chatHub.invoke('joinChat', vm.chatId)
                        .then(resp => {
                            //debugger;
                        })
                        .catch((err) => {
                            alert(err);
                        });
                });

                await start.catch(function (err) {
                    alert(err);
                });
            },
            async closeChatHub() {
                let vm = this;

                if (vm.chatHub !== null && vm.chatHub !== undefined) {
                    //  disconnect to chat group
                    await vm.chatHub.invoke('leaveChat', vm.chatId).then(
                        function (resp) {
                            vm.chatHub.stop().then(function (resp) {
                                //debugger;
                            }, function (err) {
                                alert(err);
                            });
                        }, (err) => {
                            alert(resp);
                        });
                }
            },
            async open(chatId) {
                let vm = this;

                vm.reset();

                vm.chatId = chatId;

                await vm.get();
                await vm.openChatHub();

                vm.$refs.modal.show();


            },

            async close_internal() {
                let vm = this;

                vm.common.$emit('event:close-chat');
            },
            async close() {
                let vm = this;

                await vm.closeChatHub();
                await vm.markChatAsRead();

                vm.$refs.modal.hide();
            },
            async get() {
                let vm = this;

                await vm.common.axios.get(`api/chat/${vm.chatId}`)
                    .then(resp => {
                        vm.item = resp.data;
                        for (let i in vm.item.receivers) {
                            let item = vm.item.receivers[i];

                            item.isOwner = item.userId === vm.uid;
                            item.owner = item.isOwner ? 'You' : `${item.firstName} ${item.lastName}`;
                            item.urlPicture = item.profilePicture32 ?
                                `api/files/${item.profilePicture32}/stream` : `https://via.placeholder.com/64/000000/FFFFFF/?text=${item.initials}`;
                        }

                    });

                await vm.getMessages();


            },
            async getMessages() {
                let vm = this;
                let query = `?criteria=&pageIndex=1&pageSize=99`;

                await vm.common.axios.get(`api/chat/${vm.chatId}/messages/${query}`)
                    .then(resp => {
                        vm.messages = resp.data.items;
                        for (let i in vm.messages) {
                            let item = vm.messages[i];
                            vm.processMessage(item);
                        }
                    });
            },
            processMessage(item) {
                let vm = this;

                item.isOwner = item.sender.userId === vm.uid;
                item.owner = item.isOwner ? 'You' : `${item.sender.firstName} ${item.sender.lastName}`;

                //  get profile picture
                let receiver = vm.item.receivers.find(p => p.userId == item.sender.userId);

                item.urlPicture = receiver ? receiver.urlPicture
                    : `https://via.placeholder.com/64/000000/FFFFFF/?text=${item.sender.initials}`;
            },
            async send() {
                let vm = this;

                let payload = {
                    chatId: vm.chatId,
                    content: vm.content
                };

                await vm.common.axios.post(`api/chat/message`, payload);

                vm.common.$emit('event:chat-sent');
                vm.content = null;
                vm.focusContent();
                vm.scrollToBottom();
            },
            async leave() {
                let vm = this;

                await vm.common.axios.post(`api/chat/${vm.chatId}/remove`)
                    .then(resp => {
                        vm.close();
                    })
            },
            async markChatAsRead() {
                let vm = this;

                if (vm.item.hasPendingMessage) {
                    await vm.common.axios.post(`api/chat/${vm.chatId}/markAsRead`);
                    vm.common.$emit('event:chat-marked-as-read', vm.chatId);
                }

            },
            scrollToBottom() {
                let vm = this;

                let el = vm.$refs.messages;
                if (el) {
                    el.scrollTop = el.scrollHeight;
                }
            },
            focusContent() {
                this.$refs.content.focus();
            }
        }
    }
</script>