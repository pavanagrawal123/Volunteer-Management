import { getField, updateField } from 'vuex-map-fields';
import Vuex from 'vuex'
import Vue from 'vue'

Vue.use(Vuex);

const createStore = () => {
    return new Vuex.Store({
        state: {
            // We add one empty address to
            // render the first form row.
            info: 
                {
                    firstName: '',
                    lastName: '',
                    grade: 9,
                    current: false,
                    phoneNumber: '',
                    user_id: ''
                },
        },
        getters: {
            getField,
        },
        mutations: {
            updateField,
            // We'll use this mutation to
            // dynamically add new rows.
            updateCurrent(state, field) {
                state.info.current = field;
            },
            updateUserID(state, field) {
                state.info.user_id = field;
            }
        },
        actions: {}
    })
}

export default createStore
