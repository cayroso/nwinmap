

'use strict';

import * as signalR from '@aspnet/signalr';

//import ModalViewNotification from '../Modals/Notifications/view.vue';
//import ModalViewChat from '../Modals/Chat/view.vue';
//import ModalViewQbDeposit from '../Modals/Quickbooks/quickViewQbDeposit.vue';
//import ModalQuickViewPatient from '../Modals/Accounts/quickViewPatient.vue';
//import ModalQuickViewCaregiver from '../Modals/Accounts/quickViewCaregiver.vue';

export default {
    components: {
        //ModalViewNotification,
        //ModalViewChat,
        //ModalViewQbDeposit,
        //ModalQuickViewPatient,
        //ModalQuickViewCaregiver,
    },
    data() {
        return {
            
            toggled: false,
            chatHub: null,
            notificationHub: null,
            currentOpenChatId: null,
            currentOpenNotificationId: null
        };
    },
    async created() {
        const vm = this;

        vm.toggled = vm.existingSidebar();

        //await Promise.all([vm.connectChatHub(), vm.connectNotificationHub()]);

        vm.$bus.$on('event:toggle-sidebar', vm.onToggleSidebar);

        vm.$bus.$on('event:open-notification', vm.onOpenNotification);
        vm.$bus.$on('event:close-notification', vm.onCloseNotification);

        vm.$bus.$on('event:open-chat', vm.onOpenChat);
        vm.$bus.$on('event:close-chat', vm.onCloseChat);
        vm.$bus.$on('event:send-message', vm.onSendMessage);

        vm.$bus.$on('event:quick-view-account', vm.onQuickViewAccount);

        vm.$bus.$on('event:quick-view-qb-transaction', vm.onQuickViewQbTransaction);
    },

    methods: {
        async connectChatHub() {
            let vm = this;

            vm.chatHub = new signalR.HubConnectionBuilder()
                .withUrl('/chatHub')
                .build();

            let start = vm.chatHub.start();

            await start.then(function () {

                vm.chatHub.on('chatMessageReceived', function (resp) {

                    if (vm.currentOpenChatId === null || vm.currentOpenChatId !== resp.chatId) {
                        let sender = resp.sender;

                        const h = vm.$createElement;
                        const vNodesMsg = h(
                            'div',
                            [
                                h('span', `You received a message from ${sender.firstName} ${sender.lastName}`),
                                h('br'),
                                h('br'),
                                h('a', {
                                    attrs: {
                                        style: 'cursor:pointer'
                                    }, on: {
                                        click: () => vm.$bus.sendMessage(sender.userId)
                                    }
                                }, 'Click here to open.')
                            ]
                        );
                        vm.$bvToast.toast([vNodesMsg], {
                            title: `New Message`,
                            solid: true,
                            variant: 'info',
                            //noAutoHide: true,
                        });

                        //vm.$bvToast.toast(`Message from ${sender.firstName} ${sender.lastName}`, {
                        //    title: `New Message`,
                        //    variant: 'info',
                        //    solid: true
                        //});
                    }

                    vm.$bus.$emit('event:chat-message-received', resp);
                });

            });

            await start.catch(function (err) {
                alert(error);
            });
        },

        async connectNotificationHub() {
            const vm = this;

            vm.notificationHub = new signalR.HubConnectionBuilder()
                .withUrl('/notificationHub')
                .build();

            let start = vm.notificationHub.start();

            await start.then(function () {
                vm.notificationHub.on('receive', function (resp) {
                    vm.$bvToast.toast(`${resp.content}`, {
                        title: `${resp.subject}`,
                        variant: 'info',
                        solid: true
                    });

                    //  emit event
                    vm.$bus.$emit('event:notification-received', resp.notificationId);
                });

            });

            await start.catch(function (err) {
                vm.$util.handleError(error);
            });
        },

        onToggleSidebar() {
            const vm = this;

            const existing = vm.existingSidebar();

            if (vm.toggled !== existing) {
                return;
            }

            vm.toggled = !vm.toggled;

            localStorage.setItem('sidebar:toggled', vm.toggled);
        },

        existingSidebar() {
            return localStorage.getItem('sidebar:toggled') === 'true' || false;
        },

        async onOpenNotification(notificationId) {
            const vm = this;

            vm.currentOpenNotificationId = notificationId;
            vm.$refs.modalViewNotification.open(notificationId);
        },
        async onCloseNotification() {
            const vm = this;

            vm.currentOpenNotificationId = null;
            vm.$refs.modalViewNotification.close();
        },

        async onSendMessage(userId) {
            const vm = this;

            try {
                await vm.$util.axios.post(`api/chat/add/${userId}`)
                    .then(async resp => {
                        await vm.onOpenChat(resp.data);
                    });
            } catch (e) {
                vm.$util.handleError(e);
            }
        },
        async onOpenChat(chatId) {
            const vm = this;

            vm.currentOpenChatId = chatId;
            vm.$refs.modalViewChat.open(chatId);
        },
        async onCloseChat() {
            const vm = this;

            vm.currentOpenChatId = null;
            vm.$refs.modalViewChat.close();
        },

        async onQuickViewAccount(accountId) {
            const vm = this;

            try {

                await vm.$util.axios(`api/accounts/${accountId}/account-type`)
                    .then(resp => {
                        var accountType = resp.data;

                        switch (accountType) {
                            //  patient
                            case 1:
                                vm.$refs.modalQuickViewPatient.open(accountId);
                                break;
                            //  caregiver
                            case 2:
                                vm.$refs.modalQuickViewCaregiver.open(accountId);
                                break;
                        }
                    });
            } catch (e) {
                vm.$util.handleError(e);
            }
        },

        async onQuickViewQbTransaction(id) {
            const vm = this;

            vm.$refs.modalViewQbDeposit.open(id);
        }
    }
}
