## MarsXPLR top level

import os
import shutil

APPNAME = 'MarsXPLR'
VERSION = '4.0.3'

top = '.'
out = 'wafbuild'

def options(opt):
	opt.add_option('--mono_home',
		action='store',
		default='',
		help='path where a mono install contains bin, lib, and include directories')
	opt.add_option('--self_hosted',
		action='store',
		default='no',
		help='specify this as \"no\" to make a build that can be copied into a legacy build of marsxplr')
	
	opt.recurse('MarsRuntime')

def configure(conf):
	conf.env.MONO_HOME = conf.options.mono_home
	if conf.env.MONO_HOME != '':
		conf.find_program('mcs',
			var='MCS',
			path_list=[os.path.join(conf.env.MONO_HOME, 'bin')])
	else:
		conf.find_program('mcs', var='MCS')
		conf.env.MONO_HOME = os.path.split(
			os.path.split(
				conf.env.MCS[0]
			)[0]
		)[0]
	
	conf.env.SELF_HOSTED = conf.options.self_hosted
	if conf.env.SELF_HOSTED == 'no':
		print('TAKE NOTE: this build is configured to not be self hosted, meaning it can not run as is and must be copied into a legacy copy of marsxplr')

	conf.recurse('MarsRuntime')

def build(bld):
	if bld.cmd == 'clean':
		mbuild = os.path.join(bld.top_dir, 'marsxplr_build')
		if os.path.exists(mbuild):
			shutil.rmtree(mbuild)
		rbuild = os.path.join(bld.top_dir, 'ripper_build')
		if os.path.exists(rbuild):
			shutil.rmtree(rbuild)
		ebuild = os.path.join(bld.top_dir, 'editor_build')
		if os.path.exists(ebuild):
			shutil.rmtree(ebuild)
		return
	
	bld.env.MONOOPTS = '-sdk:2 -langversion:3'

	if(bld.env.SELF_HOSTED == 'yes'):
		bld.recurse('MarsRuntime')
	
	bld.recurse('UnityDomainLoad')
	bld.recurse('UnityEngine')
	bld.add_group()
	bld.recurse('Assembly_-_CSharp_-_first_pass')
	bld.add_group()
	bld.recurse('Assembly_-_CSharp')
	bld.recurse('Assembly_-_UnityScript')
	bld.recurse('ripper')
	bld.recurse('editor')
