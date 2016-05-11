from django.shortcuts import render, redirect
from django.http import Http404
from django.contrib.auth import authenticate, login as l, logout as lout
from django.contrib.auth.decorators import login_required
import requests

# api_url = "http://kursproj-001-site1.itempurl.com/api/"
api_url = "http://localhost:62164/api/"

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


def support(request):
    context = {}
    if request.user.is_authenticated():
        context['is_auth'] = True
        cart_preview = calculate_cart_preview(request.user.id)
        context['in_cart'] = cart_preview['in_cart']
        context['cart_price'] = cart_preview['cart_price']
    else:
        context['is_auth'] = False

    response = requests.get(api_url+"Consultants")
    consultants = response.json()
    response = requests.get(api_url+"Categories")
    categories = response.json()

    context['consultants'] = consultants
    context['categories'] = categories

    return render(request, 'polls/support.html', context)


def login(request):
    context = {}
    response = requests.get(api_url+"Categories")
    categories = response.json()
    context['categories'] = categories
    if request.user.is_authenticated():
        context['is_auth'] = True
        cart_preview = calculate_cart_preview(request.user.id)
        context['in_cart'] = cart_preview['in_cart']
        context['cart_price'] = cart_preview['cart_price']
    else:
        context['is_auth'] = False

    if request.method == 'POST':
        username = request.POST['username']
        password = request.POST['password']
        user = authenticate(username=username, password=password)

        if user is None:
            context['username'] = username
            context['password'] = password
            context['error'] = 'Не верно введен логин или пароль'
            return render(request, 'polls/login.html', context)

        else:
            l(request, user)
        return redirect('polls.views.index')

    else:
        context['username'] = ''
        context['password'] = ''
        context['error'] = ''
        return render(request, 'polls/login.html')


def logout(request):
    lout(request)
    return redirect('polls.views.index')


@login_required
def add_to_cart(request):
    user_id = request.user.id
    device_id = request.GET['device_id']

    api_request = {'Id': 0, 'UserId': user_id, 'DeviceId': device_id, 'Quantity': 1, 'IsSold': False}
    response = requests.post(api_url+"Carts", data=api_request)
    cart = response.json()
    if cart['Id'] > 0:
        device_url = api_url+"Devices/%s" % cart['DeviceId']
        response = requests.get(device_url)
        device = response.json()
        device['FreeCount'] = device['FreeCount'] - 1
        responce = requests.put(device_url, data=device)
    return redirect('polls.views.index')


@login_required
def cart(request):
    context = {}
    context['is_auth'] = True

    cart_preview = calculate_cart_preview(request.user.id)
    context['in_cart'] = cart_preview['in_cart']
    context['cart_price'] = cart_preview['cart_price']

    cart_devices = []
    carts_url = api_url+"Carts?userId=%s" % request.user.id
    response = requests.get(carts_url)
    carts = response.json()
    for cart_object in carts:
        device_url = api_url+"Devices/%s" % cart_object['DeviceId']
        response = requests.get(device_url)
        device_object = response.json()
        cart_device = {'Name': device_object['Model'], 'Quantity': cart_object['Quantity'],
                       'Price': cart_object['Quantity'] * device_object['Price'], 'Id': cart_object['Id']}
        cart_devices.append(cart_device)

    context['cart_devices'] = cart_devices
    response = requests.get(api_url+"Categories")
    categories = response.json()
    context['categories'] = categories

    return render(request, 'polls/cart.html', context=context)


@login_required
def delete_from_cart(request, id):
    delete_cart_url = api_url+"Carts/%s" % id
    response = requests.delete(delete_cart_url)
    cart_object = response.json()

    device_url = api_url+"Devices/%s" % cart_object['DeviceId']
    response = requests.get(device_url)
    device_object = response.json()
    device_object['FreeCount'] = device_object['FreeCount'] + 1
    responce = requests.put(device_url, data=device_object)

    return redirect('polls.views.cart')


@login_required
def pay(request):
    cart_devices = []
    carts_url = api_url+"Carts?userId=%s" % request.user.id
    response = requests.get(carts_url)
    carts = response.json()

    for cart_obj in carts:
        cart_obj['IsSold'] = True
        update_carts_url = api_url+"Carts/%s" % cart_obj['Id']
        responce = requests.put(update_carts_url, data=cart_obj)

    return redirect('polls.views.cart')


@login_required
def add_review(request):
    carts_url = api_url + "Reviews"
    device_id = request.POST['device_id']
    review = request.POST['review']

    review = {"Id": 0, "DeviceId": device_id, "Content": review}
    response = requests.post(carts_url, data=review)
    return redirect('polls.views.index')


def calculate_cart_preview(user_id):
    result = {}

    carts_url = api_url+"Carts?userId=%s" % user_id
    response = requests.get(carts_url)
    carts = response.json()
    result['in_cart'] = len(carts)
    price = 0
    for cart_obj in carts:
        device_url = api_url+"Devices/%s" % cart_obj['DeviceId']
        response = requests.get(device_url)
        device_obj = response.json()
        price = price + cart_obj['Quantity'] * device_obj['Price']

    result['cart_price'] = price

    return result


def register(request):
    context = {}
    if request.user.is_authenticated():
        context['is_auth'] = True
        cart_preview = calculate_cart_preview(request.user.id)
        context['in_cart'] = cart_preview['in_cart']
        context['cart_price'] = cart_preview['cart_price']
    else:
        context['is_auth'] = False

    response = requests.get(api_url+"Categories")
    categories = response.json()
    context['categories'] = categories

    if request.method == 'POST':
        username = request.POST['username']
        password = request.POST['password']
        repeat_password = request.POST['repeat_password']
        context['username'] = username
        context['password'] = password
        context['repeat_password'] = repeat_password

        user_url = api_url+"Users/0/?name=%s" % username
        response = requests.get(user_url)

        if response.status_code != 404:
            context['error'] = 'Пользователь с таким именем уже добавлен'
            return render(request, 'polls/register.html', context)

        if password != repeat_password:
            context['error'] = 'Пароли не совпатают'
            return render(request, 'polls/register.html', context)

        user = {'Id': 0, 'Name': username, 'Password': password}

        create_user_url = api_url+"Users"
        response = requests.post(create_user_url, data=user)
        user = response.json()

        if user['Id'] < 1:
            context['error'] = 'Не удалось создать пользователя'
            return render(request, 'polls/register.html', context)

        user = authenticate(username=username, password=password)

        if user is not None:
            l(request, user)

            return redirect('polls.views.index')
    else:
        return render(request, 'polls/register.html')


