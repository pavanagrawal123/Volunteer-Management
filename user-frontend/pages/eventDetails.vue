<template>

  <v-layout
  child-flex
    column
    justify-center
    align-center>
    <v-btn @click="back" small>Back</v-btn>
       <v-dialog
      v-model="dialog"
      max-width="290"
    >
      <v-card>

        <v-card-text>
          {{this.response}}
        </v-card-text>
<v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="primary"
            flat
            @click.native="confirm(); dialog = false"
          >
            Confirm
          </v-btn>
          <v-btn
            color="primary"
            flat
            @click="dialog = false"
          >
            Go back
          </v-btn>
        </v-card-actions>
      
      </v-card>
    </v-dialog>
         <v-data-table
    :headers="headers"
    :items="events"
    hide-actions
    class="elevation-1"
    disable-initial-sort

  >
      
    <template slot="items" slot-scope="props">
      <td>{{ props.item.name }}</td>
      <td class="text-xs">{{ props.item.date }}</td>
      <td class="text-xs">{{ props.item.status }}</td>
      <td class="text-xs"><v-btn @click.native="cancel(props.item.event_id, props.item.signup_id)">Cancel</v-btn></td>
    </template>
  </v-data-table>
 
          </v-layout>
  
</template>

<script>
import axios from 'axios'
import { mapMutations } from 'vuex'
import { mapFields } from 'vuex-map-fields'

export default {
  data() {
    return {
      headers: [
        { text: 'Event Title', value: 'name' },
        { text: 'Date', value: 'date' },
        { text: 'Signup Status', value: 'status' },
        { text: 'Cancel', value: 'cancel' }
      ],
      events: [],
      dialog: false,
      response: '',
      toCancel: '',
    }
  },
  mounted() {
    this.getData()
  },
  middleware: 'member',
  computed: {
    // The `mapFields` function takes an array of
    // field names and generates corresponding
    // computed properties with getter and setter
    // functions for accessing the Vuex store.
  },
  methods: {
    getData: function() {
      $nuxt.$loading.start()
      axios
        .post(process.env.baseUrl + '/api/getSignedUp', {
          user_id: this.$store.state.info.user_id
        })
        .then(response => {
          $nuxt.$loading.finish()
          this.events = response.data.res
        })
        .catch(function(error) {
          console.log(error)
        })
    },
    back: function() {
      this.$router.go(-1)
    },
    cancel: function(eventId, signupId) {
      this.toCancel = signupId;
      $nuxt.$loading.start()
      axios
        .get(process.env.url + `/api/events/${eventId}/cancelPenalty`, {
          event_id: eventId,
          user_id: this.$store.state.info.user_id
        })
        .then(response => {
          $nuxt.$loading.finish()
          this.dialog = true
          this.response = response.data
          console.log(response.data)
        })
    },
    confirm: function() {
      $nuxt.$loading.start()
      axios
         .post(process.env.baseUrl + '/api/removeSignup', {
          _id: this.toCancel
        })
        .then(response => {
          $nuxt.$loading.finish()
          console.log(response.data)
        })
    }
  }
}
</script>