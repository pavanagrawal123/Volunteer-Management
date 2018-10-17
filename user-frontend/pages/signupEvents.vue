<template>
  <v-layout
    column
    justify-center
    align-center>
    <v-btn @click="back" small>Back</v-btn>
            <v-flex
              v-for="event in events"
              :key="event._id"
            >
      <v-card>
        <v-img
          :src=event.image
          aspect-ratio="2.75"
        ></v-img>

        <v-card-title primary-title>
          <div>
            <h3 class="headline mb-0">{{event.name}} - {{event.date}}</h3>
            <div>{{event.transportation}} - {{event.description}}</div>
          </div>
        </v-card-title>

        <v-card-actions>
          <v-btn @click="signup(event._id)" flat color="orange">Signup</v-btn>
           <v-btn @click="list(event._id)" flat color="orange">View People Signed Up</v-btn>
        </v-card-actions>
      </v-card>
    </v-flex>
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
            @click="dialog = false"
          >
            Close
          </v-btn>
        </v-card-actions>
      
      </v-card>
    </v-dialog>
          </v-layout>
  
</template>

<script>
import axios from 'axios'
import { mapMutations } from 'vuex'
import { mapFields } from 'vuex-map-fields'

export default {
  data: () => {
    return {
      events: [],
      dialog: false,
      response: ''
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
        .post(
          process.env.baseUrl + '/api/getEventsForRegistration'
        )
        .then(response => {
           $nuxt.$loading.finish()
          this.events = response.data.res
        })
        .catch(function(error) {
          console.log(error)
        })
    },
    signup: function(eventId) {
       $nuxt.$loading.start()
      axios
        .post(process.env.baseUrl + '/api/signupEvent', {
          event_id: eventId,
          user_id: this.$store.state.info.user_id
        })
        .then(response => {
           $nuxt.$loading.finish()
          this.response = response.data.res;
          this.dialog = true;
          console.log(response.data)
        })
    },
    list: function(eventId) {
      this.$router.push('/eventLists/' + eventId);
    },
    back: function() {
      this.$router.go(-1)
    }
  }
}
</script>