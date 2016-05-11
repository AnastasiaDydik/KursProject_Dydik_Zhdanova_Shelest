from django.conf.urls import url

from . import views

urlpatterns = [
    url(r'^$', views.index, name='index'),
    url(r'^device/(?P<id>[0-9]+)$', views.device, name='device'),
    url(r'^support$', views.support, name='support'),
    url(r'^login$', views.login, name='login'),
    url(r'^logout$', views.logout, name='logout'),
    url(r'^add_to_cart$', views.add_to_cart, name='add_to_cart'),
    url(r'^cart$', views.cart, name='cart'),
    url(r'^delete_from_cart/(?P<id>[0-9]+)$', views.delete_from_cart, name='delete_from_cart'),
    url(r'^pay$', views.pay, name='pay'),
    url(r'^register$', views.register, name='register'),
    url(r'^add_review$', views.add_review, name="add_review")
]
