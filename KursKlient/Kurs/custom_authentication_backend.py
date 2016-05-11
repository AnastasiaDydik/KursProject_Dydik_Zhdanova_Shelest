from django.contrib.auth.models import User
import requests


def get_session_auth_hash():
    return ''


def save(update_fields=['last_login']):
    pass


api_url = "http://kursproj-001-site1.itempurl.com/api/"


class KursAuthBackend(object):

    def authenticate(self, username=None, password=None):
        user_url = api_url+"Users/0/?name=%s" % username
        response = requests.get(user_url)
        if response.status_code == 404:
            return None

        user_object = response.json()

        if user_object is not None:
            if user_object['Password'] == password:
                user = User(username=user_object['Name'], password=user_object['Password'], id=user_object['Id'])
                user.id = user_object['Id']
                user.save = save
                return user
        return None

    def get_user(self, user_id):
        try:
            user_url = api_url+"Users/%s" % user_id
            response = requests.get(user_url)

            if response.status_code == 404:
                return None

            user_object = response.json()
            user = User(username=user_object['Name'], password=user_object['Password'], id=user_object['Id'])
            user.id = user_object['Id']
            user.save = save
            return user
        except User.DoesNotExist:
            return None
