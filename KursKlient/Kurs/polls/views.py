from django.shortcuts import render, redirect
from django.http import Http404
from django.contrib.auth import authenticate, login as l, logout as lout
from django.contrib.auth.decorators import login_required
import requests

api_url = "http://kursproj-001-site1.itempurl.com/api/"

def index(request):
    context = {}

    if request.user.is_authenticated():
        context['is_auth'] = True
        cart_preview = calculate_cart_preview(request.user.id)
        context['in_cart'] = cart_preview['in_cart']
        context['cart_price'] = cart_preview['cart_price']
    else:
        context['is_auth'] = False

    category = ''
    minPrice = ''
    maxPrice = ''
    keyword = ''
    if request.GET.__contains__('category'):
        category = request.GET['category']
    if request.GET.__contains__('minPrice'):
        minPrice = request.GET['minPrice']
    if request.GET.__contains__('maxPrice'):
        maxPrice = request.GET['maxPrice']
    if request.GET.__contains__('keyword'):
        keyword = request.GET['keyword']

    device_url = api_url+"Devices/?cat=%s&minPrice=%s&maxPrice=%s&keyword=%s&isActual=true" % (
        category, minPrice, maxPrice, keyword)
    response = requests.get(device_url)
    devices = response.json()
    response = requests.get(api_url+"Categories")
    categories = response.json()
    context['devices'] = devices
    context['categories'] = categories

    return render(request, 'polls/index.html', context)


def device(request, id):
    context = {}
    if request.user.is_authenticated():
        context['is_auth'] = True
        cart_preview = calculate_cart_preview(request.user.id)
        context['in_cart'] = cart_preview['in_cart']
        context['cart_price'] = cart_preview['cart_price']
    else:
        context['is_auth'] = False

    device_url = api_url+"Devices/%s" % id
    response = requests.get(device_url)
    device_object = response.json()
    # print(device_object)
    if device_object is None:
        return Http404('Устройство не найдено')

    category_url = api_url+"Categories/%s" % device_object['CategoryId']
    response = requests.get(category_url)
    category = response.json()

    context['device'] = device_object
    context['category'] = category

    color_url = api_url+"Colors/%s" % device_object['ColorId']
    response = requests.get(color_url)
    color = response.json()

    if color is not None:
        context['color'] = color

    maker_url = api_url+"Makers/%s" % device_object['MakerId']
    response = requests.get(maker_url)
    maker = response.json()
    context['maker'] = maker

    screen_url = api_url+"ScreenResolutions/%s" % device_object['ScreenResolutionId']
    response = requests.get(screen_url)
    screen = response.json()

    if screen is not None:
        context['screen'] = screen

    processor_url = api_url+"Processors/%s" % device_object['ProcessorId']
    response = requests.get(processor_url)
    processor = response.json()

    if processor is not None:
        context['processor'] = processor

    os_url = api_url+"OperatingSystems/%s" % device_object['OperatingSystemId']
    response = requests.get(os_url)
    os = response.json()

    if os is not None:
        context['os'] = os

    camera_url = api_url+"DigitalCameras/%s" % device_object['DigitalCameraId']
    response = requests.get(camera_url)
    camera = response.json()

    if camera is not None:
        context['camera'] = camera

    country_url = api_url+"Countries/%s" % device_object['CountryId']
    response = requests.get(country_url)
    country = response.json()
    context['country'] = country

    reviews_url = api_url+"Reviews/?deviceId=%s" % device_object['Id']
    response = requests.get(reviews_url)
    reviews = response.json()
    context['reviews'] = reviews

    response = requests.get(api_url+"Categories")
    categories = response.json()
    context['categories'] = categories

    return render(request, 'polls/device.html', context)

